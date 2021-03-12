using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsDal
{
	/// <summary>
	/// Nest 5.5.0 依赖newtonsoft.json 10.0.1
	/// </summary>
    public class EsHelper
    {
		private static readonly int MaxConnections = 100;

		/// <summary>
		/// 查询数据Client
		/// </summary>
		public static IElasticClient Client { get; set; }

		public static void Init(string esGetIp, int esGetPort)
		{
			Client = CreateEsClient(esGetIp, esGetPort);
		}

		public static Uri CreateBaseUri(string host = null, int? port = null)
		{
			var uri = new UriBuilder("http", host, port.GetValueOrDefault(9200)).Uri;
			return uri;
		}

		public static IConnectionPool CreateConnectionPool(string host = null, int? port = null)
		{
			return new SingleNodeConnectionPool(CreateBaseUri(host, port));
		}

		public static ConnectionSettings CreateEsConnection(string host = null, int? port = null)
		{
			var connectionPool = CreateConnectionPool(host, port);
			//var connectionsettings = new ConnectionSettings(connectionPool, sourceSerializer: JsonNetSerializer.Default)
			var connectionsettings = new ConnectionSettings(connectionPool)
			   .ConnectionLimit(MaxConnections)
			   .PrettyJson(false)
			   .RequestTimeout(TimeSpan.FromMinutes(2))
			   .EnableTcpKeepAlive(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(1))
			   .DefaultFieldNameInferrer(n => n)//注：nest默认字段名首字母小写，如果要设置为与Model中一致，在创建client时按如下设置。（强烈建议使用该设置，避免造成字段不一致）
			   .EnableDebugMode()
			   .DisableDirectStreaming(true)//调试
			   .ThrowExceptions(true);

			return connectionsettings;
		}
		public static ElasticClient CreateEsClient(string host = null, int? port = null)
		{
			var connectionsettings = CreateEsConnection(host, port);
			return new ElasticClient(connectionsettings);
		}
    }
}
