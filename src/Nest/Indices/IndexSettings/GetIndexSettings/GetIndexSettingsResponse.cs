﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetIndexSettingsResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, IndexState> Indices { get; }
	}

	[JsonConverter(typeof(ResolvableDictionaryResponseJsonConverter<GetIndexSettingsResponse, IndexName, IndexState>))]
	public class GetIndexSettingsResponse : DictionaryResponseBase<IndexName, IndexState>, IGetIndexSettingsResponse
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<IndexName, IndexState> Indices => Self.BackingDictionary;
	}
}
