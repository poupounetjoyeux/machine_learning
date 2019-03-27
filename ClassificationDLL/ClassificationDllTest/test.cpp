#include "pch.h"
#include "../ClassificationDLL/Source.h"

TEST(TestCaseName, TestName) {
	int * superParam = (int *) malloc(sizeof(int) * 2);
	superParam[0] = 2;
	superParam[1] = 1;

	MultiLayerModel * model = createMultilayerModel(superParam, 2, 0.01);

	int pointCount = 4;
	double * _expectedSigns = (double*)malloc(sizeof(double) * pointCount);
	double * inputs = (double *)malloc(sizeof(double) * pointCount);

	inputs[0] = -1;
	inputs[1] = -1;
	_expectedSigns[0] = -1;

	inputs[1] = 1;
	inputs[2] = 1;
	_expectedSigns[1] = -1;

	inputs[3] = -1;
	inputs[4] = 1;
	_expectedSigns[2] = 1;

	inputs[3] = 1;
	inputs[4] = -1;
	_expectedSigns[3] = 1;


	trainModelMultilayerClassification(model, inputs, pointCount, _expectedSigns, 100);


  EXPECT_TRUE(true);
}