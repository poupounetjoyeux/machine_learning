#include "Eigen/Dense"
#include <ctime>

using Eigen::MatrixXd;
using std::srand;
using std::time;

extern "C" {

	typedef struct MultiLayerModel {
		double** neuronesResults;
		double*** w;
		double** deltas;
		int nbLayers;
		int* superParam;
		int biais;
	} MultiLayerModel;

	int signOf(double number);
	double randomDouble();
	__declspec(dllexport) double* createModel(int inputsDimension);
	__declspec(dllexport) MultiLayerModel* createMultilayerModel(int* superParam, int nbLayer, int biais);
	
	__declspec(dllexport) void trainModelLinearClassification(double* model, double* inputs, int inputsDimension, int nbInputs, int* expectedSigns, double learnStep, int nbIterations);
	__declspec(dllexport) void trainModelLinearRegression(double* model, double* inputs, int inputsDimension, int nbInputs, int* expectedSigns);
	__declspec(dllexport) void trainModelMultilayerClassification(MultiLayerModel* model, double* inputs, int inputsDimension, int nbInputs);
	__declspec(dllexport) void trainModelMultilayerRegression(MultiLayerModel* model, double* inputs, int inputsDimension, int nbInputs);
	
	__declspec(dllexport) int predictClassificationModel(double* model, double* inputk, int inputsDimension);
	__declspec(dllexport) double predictRegressionModel(double* model, double* inputk, int inputsDimension);
	__declspec(dllexport) int predictMultilayerClassificationModel(MultiLayerModel* model, double* inputk, int inputsDimension);
	__declspec(dllexport) double predictMultilayerRegressionModel(MultiLayerModel* model, double* inputk, int inputsDimension);
	
	__declspec(dllexport) void releaseModel(double* model);
	__declspec(dllexport) void releaseMultilayerModel(MultiLayerModel* model);
	
	__declspec(dllexport) double* createModel(int inputsDimension) {
		double *arr = (double*)malloc(sizeof(double)*inputsDimension + 1);
		
		
		for (int i = 0; i < inputsDimension + 1; i ++)
		{
			arr[i] = randomDouble();
		}
		return arr;
	}

	__declspec(dllexport) MultiLayerModel* createMultilayerModel(int* superParam, int nbLayer, int biais) {
		MultiLayerModel *model = (MultiLayerModel*)malloc(sizeof(MultiLayerModel));
		memcpy(model->superParam, superParam, sizeof(int) * nbLayer);
		model->nbLayers = nbLayer;
		model->biais = biais;
		model->neuronesResults = (double **)malloc(sizeof(double *) * (nbLayer - 1));
		model->deltas = (double **)malloc(sizeof(double *) * nbLayer - 1);
		model->w = (double***)malloc(sizeof(double**) * nbLayer - 1);
		for (int i = 0; i < nbLayer - 1; i++)
		{
			model->neuronesResults[i] = (double *)malloc(sizeof(double) * superParam[i + 1]);
			model->deltas[i] = (double *)malloc(sizeof(double) * superParam[i + 1]);
			model->w[i] = (double **)malloc(sizeof(double*) * superParam[i]);
			for (int j = 0; j < superParam[i]; j++)
			{
				model->w[i][j] = (double*)malloc(sizeof(double) * superParam[i + 1]);
				for (int k = 0; k < superParam[i + 1]; k++)
				{
					model->w[i][j][k] = randomDouble();
				}
			}
		}
		return model;
	}

	__declspec(dllexport) void trainModelLinearClassification(double* model, double* inputs, int inputsDimension, int nbInputs, int* expectedSigns, double learnStep, int nbIterations) {
		for (int i = 0; i < nbIterations; i++) {
			for (int k = 0; k < nbInputs; k++) {
				double* inputk = inputs + k * inputsDimension;
				int predictSign = predictClassificationModel(model, inputk, inputsDimension);
				int alpha = expectedSigns[k] - predictSign;
				model[0] = model[0] + learnStep * alpha;
				for (int i = 0; i < inputsDimension; i++)
				{
					model[i + 1] = model[i + 1] + learnStep * alpha * inputk[i];
				}
			}
		}
	}

	__declspec(dllexport) void trainModelLinearRegression(double* model, double* inputs, int inputsDimension, int nbInputs, int* expectedSigns) {
		MatrixXd xMatrix(nbInputs, inputsDimension + 1);
		MatrixXd yMatrix(nbInputs, 1);

		for (int i = 0; i < nbInputs; i++) {
			yMatrix(i, 0) = expectedSigns[i];
		}

		for (int i = 0; i < nbInputs; i++) {
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

	__declspec(dllexport) void trainModelMultilayerClassification(MultiLayerModel* model, double* inputs, int inputsDimension, int nbInputs) {
		
	}

	__declspec(dllexport) void trainModelMultilayeRegression(MultiLayerModel* model, double* inputs, int inputsDimension, int nbInputs) {

	}

	void processLayers() {

	}

	__declspec(dllexport) int predictClassificationModel(double* model, double* inputk, int inputsDimension) {
		double result = model[0];
		for (int i = 0; i < inputsDimension; i++) {
			result += model[i + 1] * inputk[i];
		}
		return signOf(result);
	}
	
	__declspec(dllexport) double predictRegressionModel(double* model, double* inputk, int inputsDimension) {
		double result = model[0];
		for (int i = 0; i < inputsDimension; i++) {
			result += model[i + 1] * inputk[i];
		}
		return result;
	}

	__declspec(dllexport) int predictMultilayerClassificationModel(MultiLayerModel* model, double* inputk, int inputsDimension) {
		//TODO
		return signOf(1);
	}
	
	__declspec(dllexport) double predictMultilayerRegressionModel(MultiLayerModel* model, double* inputk, int inputsDimension) {
		//TODO
		return 1.0;
	}

	__declspec(dllexport) void releaseModel(double* model)
	{
		free(model);
	}

	__declspec(dllexport) void releaseMultilayerModel(MultiLayerModel* model)
	{
		for (int i = 1; i < model->nbLayers - 1; i++)
		{
			free(model->neuronesResults[i]);
			free(model->deltas[i]);
			for (int j = 0; j < model->superParam[i]; j++)
			{
				free(model->w[i][j]);
			}
			free(model->w[i]);
		}

		free(model->neuronesResults);
		free(model->deltas);
		free(model->w);
		free(model->superParam);

		free(model);
	}

	int signOf(double number){
		return number > 0 ? 1 : -1;
	}

	double randomDouble() {
		srand(time(nullptr));
		return ((double)rand() / RAND_MAX) * 2 - 1;
	}
}