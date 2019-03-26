#include "Eigen/Dense"
#include <ctime>

using Eigen::MatrixXd;
using std::srand;
using std::time;

extern "C" {

	typedef struct MultiLayerModel {
		double** neuronesResults;
		double** w;
		double** sigmas;
		int nbLayers;
		int* superParam;
	} MultiLayerModel;

	int sgn(double number);
	__declspec(dllexport) double* create_model(int inputsDimension);
	__declspec(dllexport) void train_model_linear_classification(double* model, double* inputs, int inputsDimension, int nbInputs, int* expectedSigns, double learnStep, int nbIterations);
	__declspec(dllexport) void train_model_linear_regression(double* model, double* inputs, int inputsDimension, int nbInputs, int* expectedSigns);
	__declspec(dllexport) void train_model_multilayer_classification(double* model, double* inputs, int inputsDimension, int nbInputs, int nbLayers);
	__declspec(dllexport) int predict(double* model, double* inputk, int inputsDimension);
	__declspec(dllexport) void release_model(double* model);
	
	__declspec(dllexport) double* create_model(int inputsDimension) {
		double *arr = (double*)malloc(sizeof(double)*inputsDimension + 1);
		srand(time(nullptr));
		
		for (int i = 0; i < inputsDimension + 1; i ++)
		{
			arr[i] = ((double)rand() / RAND_MAX) * 2 - 1;
		}
		return arr;
	}

	__declspec(dllexport) MultiLayerModel create_multilayer_model(int* superParam, int nbLayer) {
		MultiLayerModel model;
		model.superParam = superParam;
		model.nbLayers = nbLayer;
		model.neuronesResults = (double **)malloc(sizeof(double *) * nbLayer);
		for (int i = 0; i < nbLayer; i++)
		{
			model.neuronesResults[i] = (double *)malloc(sizeof(double) * superParam[i]);
		}


		/*model. = (double*)malloc(sizeof(double) * totalNeurone);

	for (int i = 0; i < totalNeurone; i++)
		{
			arr[i] = ((double)rand() / RAND_MAX) * 2 - 1;
		}
		
		model.
		return ;*/
	}

	__declspec(dllexport) void train_model_linear_classification(double* model, double* inputs, int inputsDimension, int nbInputs, int* expectedSigns, double learnStep, int nbIterations) {
		for (int i = 0; i < nbIterations; i++) {
			for (int k = 0; k < nbInputs; k++) {
				double* inputk = inputs + k * inputsDimension;
				int predictSign = predict(model, inputk, inputsDimension);
				int alpha = expectedSigns[k] - predictSign;
				model[0] = model[0] + learnStep * alpha;
				for (int i = 0; i < inputsDimension; i++)
				{
					model[i + 1] = model[i + 1] + learnStep * alpha * inputk[i];
				}
			}
		}
	}

	__declspec(dllexport) void train_model_multilayer_classification(double* model, double* inputs, int inputsDimension, int nbInputs, int* superParam, int nbLayers, int totalNeurone, int biais) {
		
	}

	__declspec(dllexport) void train_model_linear_regression(double* model, double* inputs, int inputsDimension, int nbInputs, int* expectedSigns) {
		MatrixXd xMatrix(nbInputs, inputsDimension + 1);
		MatrixXd yMatrix(nbInputs, 1);

		for (int i = 0; i < nbInputs; i++) {
			yMatrix(i, 0) = expectedSigns[i];
		}

		for (int i = 0; i < nbInputs; i ++) {
			xMatrix(i, 0) = 1.0;
			for (int j = 0; j < inputsDimension; j++) {
				xMatrix(i, j + 1) = inputs[(i * 2) + j];
			}
		}

		MatrixXd result = ((xMatrix.transpose() * xMatrix).inverse() * xMatrix.transpose()) * yMatrix;

		for (int i = 0; i <= inputsDimension; i++) {
			model[i] = result(i, 0);
		}
	}

	__declspec(dllexport) int predict(double* model, double* inputk, int inputsDimension) {
		double result = model[0];
		for (int i = 0; i < inputsDimension; i++) {
			result += model[i + 1] * inputk[i];
		}
		return sgn(result);
	}

	__declspec(dllexport) void release_model(double* model)
	{
		free(model);
	}

	int sgn(double number){
		return number > 0 ? 1 : -1;
	}
}