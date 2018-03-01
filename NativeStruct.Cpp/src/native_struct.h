#pragma once

#ifdef LIBRARY_EXPORTS
#    define LIBRARY_API __declspec(dllexport)
#else
#    define LIBRARY_API __declspec(dllimport)
#endif

#ifdef __cplusplus
extern "C" {
#endif

    LIBRARY_API struct Data;
    typedef struct Data Data;

    LIBRARY_API Data* new_data();
    LIBRARY_API void free_data(Data* data);

#ifdef __cplusplus
}
#endif
