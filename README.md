# FrameSync
网络游戏帧同步，C++,C#,unity

服务器是用的Linux/C++编写的，运行方法：进入build文件夹下,输入命令./shit 8088
服务器功能：定时广播两个客户端上传的操作数据，并给这些数据加上帧号。编写思路:epoll进行IO复用，然后开一个线程来完成定时广播功能

客户端资源文件比较多太大了所以没有上传。。可运行版本是2333f.exe，要和2333fdata文件夹放在一起才能运行。
思路：操作上传至服务器，收到服务器消息再操作。很多技能需要根据帧号来进行控制，如释放技能不能走，三连击等
按p可以打开调试框。
