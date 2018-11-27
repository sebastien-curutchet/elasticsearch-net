﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IMultiTermVectorsResponse : IResponse
	{
		IReadOnlyCollection<ITermVectors> Documents { get; }
	}

	[DataContract]
	public class MultiTermVectorsResponse : ResponseBase, IMultiTermVectorsResponse
	{
		[DataMember(Name ="docs")]
		[JsonConverter(typeof(ReadOnlyCollectionJsonConverter<TermVectorsResult, ITermVectors>))]
		public IReadOnlyCollection<ITermVectors> Documents { get; internal set; } = EmptyReadOnly<ITermVectors>.Collection;
	}
}
