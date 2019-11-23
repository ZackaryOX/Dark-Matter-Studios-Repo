#pragma once

#include "PluginSettings.h"
// Resources Used: http://www.cplusplus.com/doc/tutorial/files/

class PLUGIN_API SimpleClass {
public:

	void deleteFunction();
	
	float getSanity();
	int getObject();
	float getTime();
	void writeSanity(float _Sanity);
	void writeObject(int _ObjectTotal);
	void writeTime(float _Time);
	float readTime();
	

private:
	void setSanity(float _SanityPass);
	void setObject(int _ObjectPass);
	void setTime(float _TimePass);
	float _Sanity;
	int _ObjectTotal;
	float _Time;
};
