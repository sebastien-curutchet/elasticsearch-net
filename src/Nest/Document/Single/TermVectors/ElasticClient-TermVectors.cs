﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Returns information and statistics on terms in the fields of a particular document as stored in the index.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-termvectors.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-termvectors.html</a>
		/// </summary>
		/// <typeparam name="T">The type of the document</typeparam>
		/// <param name="selector">A descriptor for the terms vector operation</param>
		ITermVectorsResponse TermVectors<T>(Func<TermVectorsDescriptor<T>, ITermVectorsRequest<T>> selector)
			where T : class;

		/// <inheritdoc />
		ITermVectorsResponse TermVectors<T>(ITermVectorsRequest<T> request)
			where T : class;

		/// <inheritdoc />
		Task<ITermVectorsResponse> TermVectorsAsync<T>(
			Func<TermVectorsDescriptor<T>, ITermVectorsRequest<T>> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class;

		/// <inheritdoc />
		Task<ITermVectorsResponse> TermVectorsAsync<T>(ITermVectorsRequest<T> request,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ITermVectorsResponse TermVectors<T>(Func<TermVectorsDescriptor<T>, ITermVectorsRequest<T>> selector) where T : class =>
			TermVectors(selector?.Invoke(new TermVectorsDescriptor<T>(typeof(T), typeof(T))));

		/// <inheritdoc />
		public ITermVectorsResponse TermVectors<T>(ITermVectorsRequest<T> request) where T : class =>
			Dispatcher.Dispatch<ITermVectorsRequest<T>, TermVectorsRequestParameters, TermVectorsResponse>(
				request,
				LowLevelDispatch.TermvectorsDispatch<TermVectorsResponse, T>
			);

		/// <inheritdoc />
		public Task<ITermVectorsResponse> TermVectorsAsync<T>(
			Func<TermVectorsDescriptor<T>, ITermVectorsRequest<T>> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class => TermVectorsAsync(selector?.Invoke(new TermVectorsDescriptor<T>(typeof(T), typeof(T))), cancellationToken);

		/// <inheritdoc />
		public Task<ITermVectorsResponse> TermVectorsAsync<T>(ITermVectorsRequest<T> request,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class =>
			Dispatcher.DispatchAsync<ITermVectorsRequest<T>, TermVectorsRequestParameters, TermVectorsResponse, ITermVectorsResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.TermvectorsDispatchAsync<TermVectorsResponse, T>
			);
	}
}
