#include<bits/stdc++.h>
using namespace std;
struct Packet{
	short len;
	int flag;
	string user,passwd;
};
#ifndef LIBFUCK_H_ 
#define LIBFUCK_H_
int mx(int a,int b);
void Debug_log(char data[],int ret);
short GetShort(char data[],int ret);
int PutShort(char data[],int ret);
int PutString(char data[],int idx,char s[],int len);
Packet GetPacket(char data[],int ret);
int access_judge(char data[],Packet ans);
int getTarget(char data[],int ret);
void getBroad(char data[],char broad[],int ret,int &broad_tol);
#endif
