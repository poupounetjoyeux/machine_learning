#include "Eigen/Dense"
#include <ctime>

using Eigen::MatrixXi;
using std::srand;
using std::time;

extern "C" {
	__declspec(dllexport) int return5() {
		return 5;
	}

	__declspec(dllexport) double* create_dataset(int size) {
		double *arr = (double*)malloc(sizeof(int)*size);
		srand(time(nullptr));
		
		for (int i = 0; i < size; i ++)
		{
			arr[i] = ((double)rand() / RAND_MAX) * 2 - 1;
		}
		return arr;
	}

	__declspec(dllexport) void train_model(double* model, double* inputs, int dimenssion, int nbData, double* results, double step, int nbIterations) {
		double* inputk;
		for (int i = 0; i < nbIterations; i++) {
			for (int k = 0; k < nbData; k++) {
				inputk = inputs + k * 2; 
			}
		}
	}

	int sgn(double number){
		return number > 0 ? 1 : -1;
	}

	__declspec(dllexport) double predict(double* model, double* inputk, int size) {
		double result = model[0];
		for (int i = 0; i < size; i++) {
			result = model[i + 1] * inputk[i];
		}
		return sgn(result);
	}
}