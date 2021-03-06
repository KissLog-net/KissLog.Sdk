﻿using KissLog.Apis.v1.Models;
using KissLog.Apis.v1.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KissLog.Apis.v1.Apis
{
    internal interface IKissLogApi
    {
        Task<ApiResult<RequestLog>> CreateRequestLogAsync(CreateRequestLogRequest request, IList<File> files = null);
        ApiResult<RequestLog> CreateRequestLog(CreateRequestLogRequest request, IList<File> files = null);

        Task<ApiResult<bool>> UploadRequestLogFilesAsync(UploadFilesRequest request);
        ApiResult<bool> UploadRequestLogFiles(UploadFilesRequest request);
    }
}
