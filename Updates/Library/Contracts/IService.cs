using System.Collections.Generic;

namespace ProductLib;

public interface IService< TResponse, TCreateReq, TUpdateReq>
    where TResponse: IResponse
    where TCreateReq: ICreateReq
    where TUpdateReq: IUpdateReq
{
    Result<string?> Create(TCreateReq req);
    Result<string?> Delete(string key);
    Result<bool> Exist(string key);
    Result<TResponse?> Read(string key);
    Result<List<TResponse>> ReadAll();
    Result<string?> Update(TUpdateReq req);
}
