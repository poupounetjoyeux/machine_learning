#ifndef Source
#define Source 

extern "C" {
	typedef struct MultiLayerModel {
		double** neuronesResults;
		double*** w;
		double** deltas;
		int nbLayers;
		int* nplParams;
		double learnStep;
	} MultiLayerModel;

	int signOf(double number);
	double randomDouble();
	void retropropagateLayersClassification(MultiLayerModel* model, double* expectedSigns);
	void retropropagateLayersRegression(MultiLayerModel* model, double* expectedSigns);
	void retropropagateModel(MultiLayerModel* model);
	void processPredictLayers(MultiLayerModel* model, double* inputk, bool isRegression);

	__declspec(dllexport) double* createModel(int inputsDimension);
	__declspec(dllexport) MultiLayerModel* createMultilayerModel(int* superParam, int nbLayer, double learnStep);

	__declspec(dllexport) void trainModelLinearClassification(double* model, double* inputs, int inputsDimension, int nbInputs, int* expectedSigns, double learnStep, int nbIterations);
	__declspec(dllexport) void trainModelLinearRegression(double* model, double* inputs, int inputsDimension, int nbInputs, double* expectedSigns);
	__declspec(dllexport) void trainModelMultilayerClassification(MultiLayerModel* model, double* inputs, int nbInputs, double* expectedSigns, int iterations);
	__declspec(dllexport) void trainModelMultilayerRegression(MultiLayerModel* model, double* inputs, int nbInputs, double* expectedSigns, int iterations);

	__declspec(dllexport) int predictClassificationModel(double* model, double* inputk, int inputsDimension);
	__declspec(dllexport) double predictRegressionModel(double* model, double* inputk, int inputsDimension);
	__declspec(dllexport) void predictMultilayerClassificationModel(MultiLayerModel* model, double* inputk, double* outputk);
	__declspec(dllexport) void predictMultilayerRegressionModel(MultiLayerModel* model, double* inputk, double* outputk);

	__declspec(dllexport) void releaseModel(double* model);
	__declspec(dllexport) void releaseMultilayerModel(MultiLayerModel* model);

}

#endif // Source

