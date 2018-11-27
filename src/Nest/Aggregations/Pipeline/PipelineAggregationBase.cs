﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IPipelineAggregation : IAggregation
	{
		[DataMember(Name ="buckets_path")]
		IBucketsPath BucketsPath { get; set; }

		[DataMember(Name ="format")]
		string Format { get; set; }

		[DataMember(Name ="gap_policy")]
		GapPolicy? GapPolicy { get; set; }
	}

	public abstract class PipelineAggregationBase : AggregationBase, IPipelineAggregation
	{
		internal PipelineAggregationBase() { }

		public PipelineAggregationBase(string name, IBucketsPath bucketsPath) : base(name) => BucketsPath = bucketsPath;

		public IBucketsPath BucketsPath { get; set; }
		public string Format { get; set; }
		public GapPolicy? GapPolicy { get; set; }
	}

	public abstract class PipelineAggregationDescriptorBase<TPipelineAggregation, TPipelineAggregationInterface, TBucketsPath>
		: DescriptorBase<TPipelineAggregation, TPipelineAggregationInterface>, IPipelineAggregation
		where TPipelineAggregation : PipelineAggregationDescriptorBase<TPipelineAggregation, TPipelineAggregationInterface, TBucketsPath>
		, TPipelineAggregationInterface, IPipelineAggregation
		where TPipelineAggregationInterface : class, IPipelineAggregation
		where TBucketsPath : IBucketsPath
	{
		IBucketsPath IPipelineAggregation.BucketsPath { get; set; }
		string IPipelineAggregation.Format { get; set; }
		GapPolicy? IPipelineAggregation.GapPolicy { get; set; }

		IDictionary<string, object> IAggregation.Meta { get; set; }

		string IAggregation.Name { get; set; }

		public TPipelineAggregation Format(string format) => Assign(a => a.Format = format);

		public TPipelineAggregation GapPolicy(GapPolicy? gapPolicy) => Assign(a => a.GapPolicy = gapPolicy);

		public TPipelineAggregation BucketsPath(TBucketsPath bucketsPath) => Assign(a => a.BucketsPath = bucketsPath);

		public TPipelineAggregation Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.Meta = selector?.Invoke(new FluentDictionary<string, object>()));
	}
}
