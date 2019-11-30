#include "SimpleClass.h"

void SimpleClass::writeObject(int _ObjectTotal) {
	std::ofstream _userData("metricsLogger.txt");
	setObject(_ObjectTotal);
	if (_userData.is_open()) {
		_userData << "Player Objects Collected: " << getObject() << "\n";
		_userData.close();
	}
}

void SimpleClass::writeTime(float _Time)
{
	std::ofstream _userData("metricsLogger.txt", std::ios::app);
	setTime(_Time);
	if (_userData.is_open()) {
		_userData << _Time << "\n";
		_userData.close();
	}
}

float SimpleClass::readTime()
{
	std::string _Time;
	std::string _PreviousTime = "NULL";
	float _TimeF;
	float _PreviousTimeF;
	std::ifstream _userData("metricsLogger.txt");
	if (_userData.is_open())
	{
		while (std::getline(_userData, _Time))
		{
			if (_PreviousTime._Equal("NULL")) {
				_TimeF = std::stof(_Time);
				_PreviousTime = _Time;
				_PreviousTimeF = std::stof(_Time);
			}
			else {
				_TimeF = std::stof(_Time);
				if (_TimeF > _PreviousTimeF) {
					_PreviousTimeF = _TimeF;

				}
				else {
					_TimeF = _PreviousTimeF;
				}
			}
			
		}
		_userData.close();
	}

	return _TimeF;
}

void SimpleClass::setSanity(float _SanityPass)
{
	_Sanity = _SanityPass;
}

void SimpleClass::setObject(int _ObjectPass)
{
	_ObjectTotal = _ObjectPass;
}

void SimpleClass::setTime(float _TimePass)
{
	_Time = _TimePass;
}

void SimpleClass::writeSanity(float _Sanity) {
	std::ofstream _userData("metricsLogger.txt");
	setSanity(_Sanity);
	if (_userData.is_open()) {
		_userData << "Player _Sanity Value: " << getSanity() << "\n";

		_userData.close();
	}
}

void SimpleClass::deleteFunction() {
	remove("metricsLogger.txt");
}

float SimpleClass::getSanity(){
	return _Sanity;
}

int SimpleClass::getObject()
{
	return _ObjectTotal;
}

float SimpleClass::getTime()
{
	return _Time;
}
