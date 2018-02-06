#include<bits/stdc++.h>
using namespace std;
map<string,string> mp;
struct Packet{
	short len;
	int flag;
	string user,passwd;
	Packet(){
		user="";
		passwd="";
	}
};
short GetShort(char data[],int ret)
{
	if(ret<2)	return -1;
	short num=0;
	for(int i=0;i<2;i++)
		num=data[i]+num*(1<<8);
	return num;
}
int PutShort(char data[],int ret)
{
	int idx=2;
	char tmp[4];
	for(int i=0;i<idx;i++){
		tmp[i]=ret%(1<<8);
		ret/=(1<<8);
	}
	for(int i=0;i<idx;i++)
		data[i]=tmp[i];
	return idx;
}
int PutString(char data[],int idx,char s[],int len)
{
	for(int i=0;i<len;i++)
		data[idx++]=s[i];
	return idx;
}	
Packet GetPacket(char data[],int ret)
{
	Packet ans;
	ans.len=GetShort(data,ret);
	int x=0;
	while(data[x]!=':')	x++;
	ans.flag=data[x-1]-'0';
	ans.user="";ans.passwd="";
	int i;
	char tmp[20];
	for(i=0;i<ret;i++){
		if(x+1<ret&&data[x+1]==':')	break;
		//ans.user.append(data[++x]);//printf("%c %d",ans.user[i],i);
		tmp[i]=data[++x];
	}
	tmp[i]='\0';
	ans.user.append(tmp);
	cout<<ans.user<<" "<<x<<endl;
	ans.user[i]='\0';++x;
	for(i=0;i<ret;i++){
		if(x+1<ret&&data[x+1]==':')	break;
		//ans.passwd.append(data[++x]);
		tmp[i]=data[++x];
	}
	tmp[i]='\0';
	ans.passwd.append(tmp);
	cout<<ans.passwd<<" "<<x<<endl;
	ans.passwd[i]='\0';
	return ans;
}
int getTarget(char data[],int ret)
{
	int x=0;
	while(data[x]!=':'&&x<ret-1)	x++;
	int idx=data[x+1]-'0';
	return idx;
}
int access_judge(char data[],Packet ans)
{
	char s1[]="登录成功";
	char s2[]="登录失败";
	char s3[]="注册成功";
	char s4[]="注册失败";
	//int idx=PutShort(data,strlen(s1));
	int len;
	int idx;
	//printf("idx %d\n",idx);
	if(ans.flag==1){
		cout<<ans.user<<" "<<ans.passwd<<endl;
		if(mp.count(ans.user)!=0&&mp[ans.user]==ans.passwd){
			idx=PutShort(data,strlen(s1));
			len=PutString(data,idx,s1,strlen(s1));
		}
		else{
			idx=PutShort(data,strlen(s2));
			len=PutString(data,idx,s2,strlen(s2));
		}
	}
	else if(ans.flag==0){
		if(mp.count(ans.user)==0){
			mp[ans.user]=ans.passwd;
			idx=PutShort(data,strlen(s3));
			len=PutString(data,idx,s3,strlen(s3));
		}
		else{
			idx=PutShort(data,strlen(s4));
			len=PutString(data,idx,s4,strlen(s4));
		}
	}
	return len;
}							
int mx(int a,int b)
{
	if(a>b)	return a;
	return b;
}
void Debug_log(char data[],int ret)
{
	int i;
	printf("%d\n",ret);
	printf("%d\n",data[1]*(1<<8)+data[0]);
	//printf("%d\n",getTarget(data,ret));
	for(i=2;i<ret;i++)
		printf("%c",data[i]);
	printf("fuck\n");
}
void getBroad(char data[],char broad[],int ret,int &broad_tol)
{
	for(int i=0;i<ret;i++)
		broad[broad_tol++]=data[i];
}
