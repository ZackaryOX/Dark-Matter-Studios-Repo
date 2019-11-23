#pragma once

#include "PluginSettings.h"
#include "SimpleClass.h"

#ifdef __cplusplus
extern "C"
{
#endif

	//Call functions here
	PLUGIN_API void writeTime(float _Time);
	PLUGIN_API float readTime();
	PLUGIN_API void deleteLogs();

#ifdef __cplusplus 
}
#endif
