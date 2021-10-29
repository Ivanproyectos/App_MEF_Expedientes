using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras;
//using MEF.Microformas.Entity.Carga.Vistas;
//using MEF.Microformas.Entity.Proceso;
//using MEF.Microformas.Entity.Proceso.Vistas;
//using MEF.Microformas.Entity.Administracion.Vistas;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
//using MEF.Microformas.Entity.Administracion;

namespace MEF.Expedientes.Data
{
    [DbConfigurationType(typeof(OracleDbConfiguration))]
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
             : base("name=DefaultConnection") => Database.SetInitializer<DatabaseContext>(null);

        public DatabaseContext(string connectionString)
            : base(connectionString) => Database.SetInitializer<DatabaseContext>(null);

        public virtual DbSet<Cls_Ent_Oficina> cls_Ent_Oficina { get; set; }
        public virtual DbSet<Cls_Ent_Personal> cls_Ent_Personal { get; set; }

        //public virtual DbSet<Cls_Ent_Tabla> cls_Ent_Tabla { get; set; }
        //public virtual DbSet<Cls_Ent_Campo> cls_Ent_Campo { get; set; }
        //public virtual DbSet<Cls_Ent_Control_Carga> cls_Ent_Control_Carga { get; set; }
        //public virtual DbSet<Cls_V_Control_Carga> cls_Ent_V_Control_Carga { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("EXPEDIENTES");

            //modelBuilder.Entity<Cls_Ent_Control_Carga>().ha.Property = false;
            //.HasSequence(PersonIdSequence)
            //.IncrementsBy(1);

            //modelBuilder.HasSequence<int>("OrderNumbers");

            //modelBuilder
            //    .Entity<Cls_Ent_Control_Carga>()
            //    .Property(x => x.ID_CONTROL_CARGA);
            //.forora(PersonIdSequence);

            //modelBuilder.Entity<Casilla>()
            //    .Property(p => p.HTML).HasColumnType("clob");

            //modelBuilder.Entity<Cls_Ent_Seccion>().HasForeignKey(s => s.ID_SECCION)
            //    .WillCascadeOnDelete(false);
            //.HasMany(p => p.CasillaArchivos)
            //.WithRequired(p => p.)


            //modelBuilder.Entity<Documento>()
            //    .HasMany(p => p.DocumentoAnexos)
            //    .WithRequired(p => p.Documento)
            //    .HasForeignKey(s => s.ID_DOCUMENTO)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Documento>()
            //    .HasMany(p => p.DocumentoObservaciones)
            //    .WithRequired(p => p.Documento)
            //    .HasForeignKey(s => s.ID_DOCUMENTO)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Documento>()
            //    .HasMany(p => p.DocumentoOficinas)
            //    .WithRequired(p => p.Documento)
            //    .HasForeignKey(s => s.ID_DOCUMENTO)
            //    .WillCascadeOnDelete(false); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        private string BuildStoredProcedureQuery(string storedProcedureName, params object[] parameters)
        {
            var query = new StringBuilder();

            query.AppendFormat("begin {0}(", storedProcedureName);

            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                if (i > 0) query.Append(",");

                if (parameters[i] is OracleParameter parameter)
                    query.AppendFormat(":{0}", parameter.ParameterName);

            }

            query.Append("); end;");
            return query.ToString();
        }


        private string BuildFunctionQuery(string storedProcedureName, params object[] parameters)
        {
            var query = new StringBuilder();

            query.AppendFormat("begin :result := {0}(", storedProcedureName);

            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                if (i > 0) query.Append(",");

                if (parameters[i] is OracleParameter parameter)
                    query.AppendFormat(":{0}", parameter.ParameterName);

            }

            query.Append("); end;");
            return query.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public int ExecuteStoreProcedureCommand(string storedProcedureName, params object[] parameterValues)
        {
            return Database.ExecuteSqlCommand(BuildStoredProcedureQuery(storedProcedureName, parameterValues),
                parameterValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public IEnumerable<T> StoreProcedureQuery<T>(string storedProcedureName, params object[] parameterValues) where T : class
        {
            return Database.SqlQuery<T>(BuildStoredProcedureQuery(storedProcedureName, parameterValues),
                parameterValues);
        }

        /// <summary>
        /// Execute function Oracle.
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="returnValue"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalarFunction(string functionName, OracleParameter returnValue, params object[] parameters)
        {
            var connection = Database.Connection;
            var command = (OracleCommand)connection.CreateCommand();
            command.CommandText = functionName;
            command.CommandType = CommandType.StoredProcedure;

            if (!returnValue.Direction.Equals(ParameterDirection.ReturnValue))
                returnValue.Direction = ParameterDirection.ReturnValue;

            // Añadir parametros a Oracle
            command.Parameters.Add(returnValue);

            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
                if ((parameters[i] is OracleParameter parameter))
                    command.Parameters.Add(parameter);

            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                connection.Open();

            command.ExecuteScalar();
            return command.Parameters[returnValue.ParameterName].Value;
        }

        /// <summary>
        /// Obtiene la secuencia ejecutada en la base de datos.
        /// </summary>
        /// <param name="sequenceName"></param>
        /// <returns></returns>
        public long GetSequence(string sequenceName)
        {
            return Database.SqlQuery<long>($"SELECT {sequenceName}.nextval FROM dual").First();
        }

        public long GetQuery(string sqlQuery)
        {
            return Database.SqlQuery<long>($"{sqlQuery}").First();
        }

        //public string GetQuery_S(string sqlQuery)
        //{
        //    return Database.SqlQuery<long>($"{sqlQuery}").First();
        //}

        /// <summary>
        /// Executa y devuelve un <see cref="DbDataReader"/>
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string storedProcedureName, params object[] parameterValues)
        {
            var connection = Database.Connection;
            var command = CreateCommand(connection, parameterValues);
            command.CommandText = storedProcedureName;
            command.CommandType = CommandType.StoredProcedure;

            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public DbCommand GetStoredProcedureCommand(string storedProcedureName, params object[] parameterValues)
        {
            var connection = Database.Connection;
            var command = CreateCommand(connection, parameterValues);
            command.CommandText = storedProcedureName;
            command.CommandType = CommandType.StoredProcedure;

            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                connection.Open();

            return command;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private DbCommand CreateCommand(DbConnection connection, params object[] parameters)
        {
            var command = connection.CreateCommand();

            if (parameters == null)
                command.Parameters.Add(GetDefaultRefCursor());

            // Añadir parametros a Oracle
            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                if (!(parameters[i] is OracleParameter parameter))
                {
                    command.Parameters.Add(GetDefaultRefCursor());
                    continue;
                }

                command.Parameters.Add(parameter);
            }
            return command;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OracleParameter GetDefaultRefCursor()
        {
            return new OracleParameter()
            {
                OracleDbType = OracleDbType.RefCursor,
                Direction = System.Data.ParameterDirection.Output
            };
        }

        public OracleConnection GetNewConnection()
        {
            string _cnSTR = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //  string sadas = System.Configuration.ConfigurationManager.AppSettings["DefaultConnection"].ToString();
            OracleConnection cn = new OracleConnection(_cnSTR);
            cn.Open();
            return cn;
        }

        public void WriteToDatabase(string COD_TABLA_TEMPORAL, DataTable dt, string[] col, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                using (OracleConnection cn = this.GetNewConnection())
                {
                    using (var bulkCopy = new OracleBulkCopy(cn, OracleBulkCopyOptions.Default)) //OracleBulkCopyOptions.Default  //UseInternalTransaction
                    {
                        bulkCopy.DestinationTableName = COD_TABLA_TEMPORAL;

                        // set the destination table name  
                        foreach (string ff in col)
                        {
                            if (ff != null)
                            {
                                OracleBulkCopyColumnMapping mapMumber = new OracleBulkCopyColumnMapping(ff, ff);
                                bulkCopy.ColumnMappings.Add(mapMumber);
                            }
                        }
                        //  SqlBulkCopyOptions.KeepNulls
                        //bulkCopy.BulkCopyOptions = OracleBulkCopyOptions.UseInternalTransaction;
                        bulkCopy.BulkCopyTimeout = 1200;

                        bulkCopy.WriteToServer(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }
    }
}
