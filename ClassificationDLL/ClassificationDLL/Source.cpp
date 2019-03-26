#include "Eigen/Dense"
#include <ctime>

using Eigen::MatrixXi;
using std::srand;
using std::time;

extern "C" {
	__declspec(dllexport) double* create_dataset(int inputsDimension) {
		double *arr = (double*)malloc(sizeof(int)*inputsDimension + 1);
		srand(time(nullptr));
		
		for (int i = 0; i < inputsDimension + 1; i ++)
		{
			arr[i] = ((double)rand() / RAND_MAX) * 2 - 1;
		}
		return arr;
	}

	__declspec(dllexport) void train_model(double* model, double* inputs, int inputsDimension, int nbInputs, double* expectedSigns, double learnStep, int nbIterations) {
		for (int i = 0; i < nbIterations; i++) {
			for (int k = 0; k < nbInputs; k++) {
				double* inputk = inputs + k * 2;
				int predictSign = predict(model, inputk, inputsDimension);
				learn(model, inputk, inputsDimension, predictSign, expectedSigns[k], learnStep);
			}
		}
	}

	int sgn(double number){
		return number > 0 ? 1 : -1;
	}

	void learn(double* model, double* inputk, int inputsDimension, int predictedSign, int expectedSign, double learnStep)
	{
		int alpha = expectedSign - predictedSign;
		model[0] = model[0] + learnStep * alpha;
		for (int i = 0; i < inputsDimension; i++)
		{
			model[i+1] = model[0] + learnStep * alpha * inputk[i];
		}
	}

	__declspec(dllexport) double predict(double* model, double* inputk, int size) {
		double result = model[0];
		for (int i = 0; i < size; i++) {
			result += model[i + 1] * inputk[i];
		}
		return sgn(result);
	}
}