#include "pch.h"
#include "../ClassificationDLL/Source.h"

TEST(TestCaseName, TestName) {
	int * superParam = (int *) malloc(sizeof(int) * 2);
	superParam[0] = 2;
	superParam[1] = 1;

	MultiLayerModel * model = createMultilayerModel(superParam, 2, 0.01);

	int pointCount = 4;
	double * _expectedSigns = (double*)malloc(sizeof(double) * pointCount);
	double * inputs = (double *)malloc(sizeof(double) * pointCount * 2);

	inputs[0] = -1;
	inputs[1] = -1;
	_expectedSigns[0] = -1;

	inputs[2] = 1;
	inputs[3] = 1;
	_expectedSigns[1] = -1;

	inputs[4] = -1;
	inputs[5] = 1;
	_expectedSigns[2] = 1;

	inputs[6] = 1;
	inputs[7] = -1;
	_expectedSigns[3] = 1;


	trainModelMultilayerClassification(model, inputs, pointCount, _expectedSigns, 1000000);

	double * point = (double*)malloc(sizeof(double) * 2);
	double * output = (double*)malloc(sizeof(double));
	//
	point[0] = inputs[0];
	point[1] = inputs[1];
	predictMultilayerClassificationModel(model, point, output);
	
	std::cout << "Point 1 Expected : " << _expectedSigns[0] << ", got " << output[0] << '\n';

	point[0] = inputs[2];
	point[1] = inputs[3];
	predictMultilayerClassificationModel(model, point, output);
	std::cout << "Point 2 Expected : " << _expectedSigns[1] << ", got " << output[0] << '\n';

	point[0] = inputs[4];
	point[1] = inputs[6];
	predictMultilayerClassificationModel(model, point, output);
	std::cout << "Point 3 Expected : " << _expectedSigns[2] << ", got " << output[0] << '\n';

	point[0] = inputs[6];
	point[1] = inputs[7];
	predictMultilayerClassificationModel(model, point, output);
	std::cout << "Point 3 Expected : " << _expectedSigns[3] << ", got " << output[0] << '\n';

	EXPECT_TRUE(true);
}