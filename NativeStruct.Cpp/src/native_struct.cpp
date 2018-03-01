#include "native_struct.h"

struct Data
{
    double objective;
    double* paramters;
    int parameterCount;
    double* objectives;
    int objectiveCount;
};

Data* new_data()
{
    auto* data = new Data
    {
        47.11,
        new double[3] { 1.0, 2.0, 3.0 },
        3,
        new double[3] { -1.0, -2.0, -3.0 },
        3
    };

    return data;
}

void free_data(Data* data)
{
    if (data == nullptr)
        return;

    delete[] data->paramters;
    data->paramters = nullptr;
    delete[] data->objectives;
    data->objectives = nullptr;

    delete data;
}
