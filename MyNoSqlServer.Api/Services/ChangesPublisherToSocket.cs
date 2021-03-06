using System.Collections.Generic;
using MyNoSqlServer.Api.Hubs;
using MyNoSqlServer.Domains.DataSynchronization;
using MyNoSqlServer.Domains.Db.Partitions;
using MyNoSqlServer.Domains.Db.Rows;
using MyNoSqlServer.Domains.Db.Tables;

namespace MyNoSqlServer.Api.Services
{
    public class ChangesPublisherToSocket : IReplicaSynchronizationService
    {

        public void PublishInitTable(DbTable dbTable)
        {
            ChangesHub.BroadCastInit(dbTable);
            ChangesTcpService.BroadcastInitTable(dbTable);
        }

        public void PublishInitPartition(DbTable dbTable, DbPartition partition)
        {
            ChangesHub.BroadCastInit(dbTable, partition);
            ChangesTcpService.BroadcastInitPartition(dbTable, partition);
        }

        public void SynchronizeUpdate(DbTable dbTable, IReadOnlyList<DbRow> dbRow)
        {
            ChangesHub.BroadcastRowsUpdate(dbTable, dbRow);
            ChangesTcpService.BroadcastRowsUpdate(dbTable, dbRow);
        }

        public void SynchronizeDelete(DbTable dbTable, IReadOnlyList<DbRow> dbRows)
        {
            ChangesHub.BroadcastDelete(dbTable, dbRows);
            ChangesTcpService.BroadcastRowsDelete(dbTable, dbRows);
        }
    }
    
}