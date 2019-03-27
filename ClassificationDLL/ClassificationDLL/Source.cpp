#include "Eigen/Dense"
#include <ctime>
#include <math.h>

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
		double learnStep;
	} MultiLayerModel;

	int signOf(double number);
	double randomDouble();
	void retropropagateLayersClassification(MultiLayerModel* model, int* expectedSigns);
	void retropropagateLayersRegression(MultiLayerModel* model, int* expectedSigns);
	void retropropagateModel(MultiLayerModel* model);
	void processPredictLayers(MultiLayerModel* model, double* inputk);

	__declspec(dllexport) double* createModel(int inputsDimension);
	__declspec(dllexport) MultiLayerModel* createMultilayerModel(int* superParam, int nbLayer, int biais, double learnStep);
	
	__declspec(dllexport) void trainModelLinearClassification(double* model, double* inputs, int inputsDimension, int nbInputs, int* expectedSigns, double learnStep, int nbIterations);
	__declspec(dllexport) void trainModelLinearRegression(double* model, double* inputs, int inputsDimension, int nbInputs, int* expectedSigns);
	__declspec(dllexport) void trainModelMultilayerClassification(MultiLayerModel* model, double* inputs, int nbInputs, int* expectedSigns);
	__declspec(dllexport) void trainModelMultilayerRegression(MultiLayerModel* model, double* inputs, int nbInputs, int* expectedSigns);
	
	__declspec(dllexport) int predictClassificationModel(double* model, double* inputk, int inputsDimension);
	__declspec(dllexport) double predictRegressionModel(double* model, double* inputk, int inputsDimension);
	__declspec(dllexport) int predictMultilayerClassificationModel(MultiLayerModel* model, double* inputk);
	__declspec(dllexport) double predictMultilayerRegressionModel(MultiLayerModel* model, double* inputk);
	
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

	__declspec(dllexport) MultiLayerModel* createMultilayerModel(int* superParam, int nbLayer, int biais, double learnStep) {
		MultiLayerModel *model = (MultiLayerModel*)malloc(sizeof(MultiLayerModel));
		model->superParam = (int*)malloc(sizeof(int) * nbLayer);
		memcpy(model->superParam, superParam, sizeof(int) * nbLayer);
		model->nbLayers = nbLayer;
		model->biais = biais;
		model->learnStep = learnStep;
		model->neuronesResults = (double **)malloc(sizeof(double *) * nbLayer);
		model->neuronesResults[0] = (double *)malloc(sizeof(double) * (superParam[0] + 1));
		model->neuronesResults[0][superParam[0]] = biais;
		int layers = nbLayer - 1;
		model->deltas = (double **)malloc(sizeof(double *) * layers);
		model->w = (double***)malloc(sizeof(double**) * layers);
		for (int i = 0; i < layers; i++)
		{
			model->neuronesResults[i+1] = (double *)malloc(sizeof(double) * (superParam[i + 1] + 1));

			//Add biais
			model->neuronesResults[i + 1][superParam[i + 1]] = biais;

			model->deltas[i] = (double *)malloc(sizeof(double) * superParam[i + 1]);
			model->w[i] = (double **)malloc(sizeof(double*) * (superParam[i] + 1));
			for (int j = 0; j < superParam[i] + 1; j++)
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

	__declspec(dllexport) void trainModelMultilayerClassification(MultiLayerModel* model, double* inputs, int nbInputs, int* expectedSigns) {
		for (int input = 0; input < nbInputs; input++)
		{
			processPredictLayers(model, inputs + (input * 2));
			retropropagateLayersClassification(model, expectedSigns + (input * 2));
		}
	}

	__declspec(dllexport) void trainModelMultilayerRegression(MultiLayerModel* model, double* inputs, int nbInputs, int* expectedSigns) {
		for (int input = 0; input < nbInputs; input++)
		{
			processPredictLayers(model, inputs + (input * 2));
			retropropagateLayersRegression(model, expectedSigns + (input * 2));
		}
	}

	void retropropagateLayersClassification(MultiLayerModel* model, int* expectedSigns) {
		for (int lastNeurone = 0; lastNeurone < model->superParam[model->nbLayers - 1]; lastNeurone++)
		{
			model->deltas[model->nbLayers - 2][lastNeurone] = (1 - pow(model->neuronesResults[model->nbLayers - 1][lastNeurone], 2)) * (model->neuronesResults[model->nbLayers - 1][lastNeurone] - expectedSigns[lastNeurone]);
		}
		retropropagateModel(model);
	}

	void retropropagateLayersRegression(MultiLayerModel* model, int* expectedSigns) {
		for (int lastNeurone = 0; lastNeurone < model->superParam[model->nbLayers - 1]; lastNeurone++)
		{
			model->deltas[model->nbLayers - 2][lastNeurone] = (model->neuronesResults[model->nbLayers - 1][lastNeurone] - expectedSigns[lastNeurone]);
		}
		retropropagateModel(model);
	}

	void retropropagateModel(MultiLayerModel* model)
	{
		for (int layer = model->nbLayers - 2; layer > 0; layer--)
		{
			for (int neur = 0; neur < model->superParam[layer]; neur++)
			{
				double sigma = 0.0;
				for (int nextNeur = 0; nextNeur < model->superParam[layer + 1]; nextNeur++)
				{
					sigma += model->w[layer][neur][nextNeur] * model->deltas[layer][nextNeur];
				}
				model->deltas[layer - 1][neur] = (1 - pow(model->neuronesResults[layer][neur], 2)) * sigma;
			}
		}
		for (int layer = 0; layer < model->nbLayers - 1; layer++)
		{
			for (int neur = 0; neur < model->superParam[layer]; neur++)
			{
				for (int nextNeur = 0; nextNeur < model->superParam[layer + 1]; nextNeur++)
				{
					model->w[layer][neur][nextNeur] = model->w[layer][neur][nextNeur] - model->learnStep * model->neuronesResults[layer + 1][nextNeur] * model->deltas[layer][nextNeur];
				}
			}
		}
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

	void processPredictLayers(MultiLayerModel* model, double* inputk)
	{
		for (int neur = 0; neur < model->superParam[0]; neur++)
		{
			model->neuronesResults[0][neur] = inputk[neur];
		}
		for (int layer = 0; layer < model->nbLayers - 1; layer++)
		{
			for (int neuroneRes = 0; neuroneRes < model->superParam[layer + 1]; neuroneRes++)
			{
				double sigma = 0.0;
				//+1 pour le biais
				for (int currentNeurone = 0; currentNeurone < model->superParam[layer] + 1; currentNeurone++)
				{
					sigma += model->neuronesResults[layer][currentNeurone] * model->w[layer][currentNeurone][neuroneRes];
				}
				model->neuronesResults[layer + 1][neuroneRes] = tanh(sigma);
			}
		}
	}

	__declspec(dllexport) int predictMultilayerClassificationModel(MultiLayerModel* model, double* inputk) {
		processPredictLayers(model, inputk);
		return signOf(model->neuronesResults[model->nbLayers - 1][0]);
	}
	
	__declspec(dllexport) double predictMultilayerRegressionModel(MultiLayerModel* model, double* inputk) {
		processPredictLayers(model, inputk);
		return model->neuronesResults[model->nbLayers - 1][0];
	}

	__declspec(dllexport) void releaseModel(double* model)
	{
		free(model);
	}

	__declspec(dllexport) void releaseMultilayerModel(MultiLayerModel* model)
	{
		for (int i = 0; i < model->nbLayers - 1; i++)
		{
			free(model->neuronesResults[i]);
			free(model->deltas[i]);
			for (int j = 0; j < model->superParam[i] + 1; j++)
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