﻿using System.Net;

namespace UzNews.WebApi.Domain.Exceptions;

public class NotFoundException : ClientException
{
    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.NotFound;
    public override string TitleMessage { get; protected set; } = string.Empty;
}