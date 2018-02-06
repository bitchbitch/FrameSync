#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <sys/types.h>
#include <sys/epoll.h>
#include <sys/time.h>
#include <errno.h>
#include <fcntl.h>
#include <unistd.h>
#include <pthread.h>
#include "libfuck.h"
using namespace std;

// int epoll_create(int size);
//
// int epoll_ctl(int epfd, int op, int fd, struct epoll_event* event);
// EPOLL_CTL_ADD
// EPOLL_CTL_MOD
// EPOLL_CTL_DEL
// typedef union epoll_data
// {
//      void* ptr;
//      int   fd;
//      __uint32_t u32;
//      __uint64_t u64;
// }epoll_data_t;
// struct epoll_event
// {
//      __unit32_t events;
//      epoll_data_t data;
// };
// EPOLLIN
// EPOLLOUT
// EPOLLPRI
// EPOLLERR
// EPOLLHUP
// EPOLLET
// EPOLLONESHOT
//
// int epoll_wait(int epfd, struct epoll_event* events, int maxevents, int timeout);
//
#define MAXEVENTS 64
int set_fd[65535];
int tol=0;
char broad[65535];
char send_broad[65535];
int broad_tol;
long int local_time;
long int last_time=0;
int cur_frame=0;
pthread_mutex_t mutex;
void broadcast()
{
	if(tol<1){
		broad_tol=0;
		return;
	}
	cur_frame++;
        int frame_tmp=cur_frame;
	char s[45];int idx=0;while(frame_tmp>0){s[idx++]=frame_tmp%10+'0';frame_tmp/=10;}
	//printf("%d ",idx);
	for(int i=0;i<idx/2;i++){
		char tmp=s[i];
		s[i]=s[idx-i-1];
		s[idx-i-1]=tmp;		
	}
	//for(int i=0;i<idx;i++)printf("%c",s[i]);printf("\n");
	char p[48]="";int idy=0;idy=PutShort(p,idx);idy=PutString(p,idy,s,idx);
	//getBroad(p,broad,idy,broad_tol);
	//printf("%d\n",frame_tmp);
	//int shit=getTarget(broad,broad_tol);printf("%d\n",shit);
	//if(tol>0)	printf("%d\n",broad_tol);
	//if(tol<2){
	//	broad_tol=0;
	//	return;
	//}
	//printf("%d\n",broad_tol);
	/*int len_tmp=7;
	broad[broad_tol+1]=(len_tmp&0xff);
	len_tmp>>=8;
	broad[broad_tol]=len_tmp;
	broad_tol+=2;
	broad[broad_tol++]='$';	
	for(int i=broad_tol;i<broad_tol+4;i++){
		broad[i]=(frame_tmp&0xff);
		frame_tmp>>=8;
	}
	broad_tol+=4;
	broad[broad_tol++]='#';*/
	//if(tol>0)for(int i=0;i<broad_tol;i++)printf("%c",broad[i]);
	//broad[broad_tol]='\0';
	int x=PutShort(send_broad,broad_tol+idy);
	int send_broad_tol=PutString(send_broad,x,p,idy);
	send_broad_tol=PutString(send_broad,send_broad_tol,broad,broad_tol);
	//printf("%d %d\n",broad_tol+idy,send_broad_tol);
	for(int i=0;i<tol;i++)
	{
		int bitch=send(set_fd[i],send_broad,send_broad_tol,0);
		//printf("fuck %d\n",bitch);
	}
	broad_tol=0;
	
}
void *thread(void *ptr)
{
	struct timeval tv;
	while(1){
		gettimeofday(&tv,NULL);
		local_time = tv.tv_sec*1000+tv.tv_usec/1000;
		if(local_time-last_time>30){
			last_time=local_time;
			pthread_mutex_lock(&mutex);
			broadcast();
			pthread_mutex_unlock(&mutex);
		}
	}
}
void run(int port)
{
    struct timeval tv;
    int listen_fd = socket(AF_INET, SOCK_STREAM, 0);
    printf("listen:%d\n", listen_fd);
    if (listen_fd == -1)
    {
        return;
    }
    // address
    struct sockaddr_in listen_addr;
    listen_addr.sin_family = AF_INET;
    listen_addr.sin_addr.s_addr = htonl(INADDR_ANY);
    listen_addr.sin_port = htons(port);
    // bind
    int bind_ret = bind(listen_fd, (struct sockaddr*)&listen_addr, sizeof(listen_addr));
    printf("bind ret:%d\n", bind_ret);
    if (bind_ret < 0 )
    {
        return;
    }
    // listen
    int listen_ret = listen(listen_fd, 5);
    printf("listen ret:%d\n", listen_ret);
    
    int epoll_fd = epoll_create(10);

    struct epoll_event event;
    event.data.fd = listen_fd;
    event.events = EPOLLIN|EPOLLET;

    int ret = epoll_ctl(epoll_fd, EPOLL_CTL_ADD, listen_fd, &event);
    if (-1 == ret)
    {
        printf("epoll ctl error\n");
        return;
    }

    struct epoll_event* events = (struct epoll_event*)malloc(sizeof(event)*MAXEVENTS);
    pthread_t id;
    pthread_mutex_init(&mutex,NULL);
    int pret = pthread_create(&id,NULL,thread,NULL);
    pthread_detach(id);
    
    while(1)
    {
        int n = epoll_wait(epoll_fd, events, MAXEVENTS, -1);
        //printf("readable fd count:%d\n", n);
        int i = 0;
        for (i = 0; i < n; ++i)
        {
            int fd = events[i].data.fd;
            int fd_events = events[i].events;
            if ((fd_events & EPOLLERR) ||
                (fd_events & EPOLLHUP) ||
                (!(fd_events & EPOLLIN)))
            {
                printf("fd:%d error\n", fd);
                close(fd);
		for(int i=0;i<tol;i++)
                                if(set_fd[i]==fd)
                                {
                                        pthread_mutex_lock(&mutex);
                                        for(int j=i;j+1<tol;j++)
                                                set_fd[j]=set_fd[j+1];
                                        tol--;
                                        pthread_mutex_unlock(&mutex);
                                        break;
                                }
		//epoll_ctl(epoll_fd,EPOLL_CTL_DEL,fd,&event);
                continue;
            }
            else if (listen_fd == fd)
            {
                //while(1)
                //{
                    // accept
                    struct sockaddr client_addr;
                    socklen_t client_addr_len = sizeof(client_addr);
                    int new_fd= accept(listen_fd, &client_addr, &client_addr_len);
		    set_fd[tol++]=new_fd;
                    printf("new socket:%d\n", new_fd);
                    if (new_fd < 0)
                    {
                        printf("new socket error\n");
                        return;
                    }
                    // set nonblock
                    // to do...
                    //
                    event.data.fd = new_fd;
                    event.events = EPOLLIN|EPOLLET;
                    int ret = epoll_ctl(epoll_fd, EPOLL_CTL_ADD, new_fd, &event);
                    if (-1 == ret)
                    {
                        printf("epoll ctl error\n");
                        return;
                    }
                //}
            }
            else
            {
                //while(1)
                //{
                    // send/recv
                    char data[1024] = { 0 };
                    if ((ret = recv(fd, data, 1024, 0)) > 0)
                    {
			//Debug_log(data,ret);
			/*if(getTarget(data,ret)==1){
				Packet ans=GetPacket(data,ret);
			//cout<<ans.user<<endl;
			//Debug_log(data,ret);
				int len=access_judge(data,ans);
			//	Debug_log(data,len);
                        	send(fd, data, len, 0);
			}
			else if(getTarget(data,ret)==2){
			//	Debug_log(data,ret);
				for(int i=0;i<tol;i++)
					if(set_fd[i]!=0)
						send(set_fd[i],data,ret,0);
			}*/
			if(getTarget(data,ret)>=3){
				pthread_mutex_lock(&mutex);
				getBroad(data,broad,ret,broad_tol);
				pthread_mutex_unlock(&mutex);
			}
                        memset(data, 0, 1024);
                    }
                    //end
                    if (ret == 0)
                    {
                        printf("socket:%d, closed\n", fd);
                        close(fd);
			for(int i=0;i<tol;i++)
				if(set_fd[i]==fd)
				{
					pthread_mutex_lock(&mutex);
					for(int j=i;j+1<tol;j++)
						set_fd[j]=set_fd[j+1];
					tol--;
					pthread_mutex_unlock(&mutex);
					break;
				}
			//epoll_ctl(epoll_fd,EPOLL_CTL_DEL,fd,&event);
                        break;
                    }
                    else if (ret == -1)
                    {
                        //if (errno == EAGAIN)
                        //{
                        //    break;
                        //}
                        printf("recv failed\n");
                    }
                //}
            }
        }
    }
    return;
}

int main(int argc, char** argv)
{
//	printf("test %d\n",mx(5,8));
    if (argc < 2)
    {
        printf("input port\n");
        return 0;
    }
    
    short port = atoi(argv[1]);
    run(port); 

    return 0;
}
