#pragma once
#include <fstream>
#include <iostream>
#include <string>
#ifdef SIMPLEPLUGIN_EXPORTS
#define PLUGIN_API __declspec(dllexport)
#elif SIMPLEPLUGIN_IMPORTS
#define PLUGIN_API __declspec(dllimport)
#else
#define PLUGIN_API
#endif