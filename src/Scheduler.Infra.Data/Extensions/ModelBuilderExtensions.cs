using Scheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Scheduler.Infra.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        #region Metodos Publicos

        public static ModelBuilder ApplyGlobalStandards(this ModelBuilder builder)
        {
            foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
            {
                if (!entityType.GetTableName()!.Contains('_'))
                    entityType.SetTableName(entityType.ClrType.Name);

                entityType.GetForeignKeys()
                          .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                          .ToList()
                          .ForEach(fe => fe.DeleteBehavior = DeleteBehavior.Restrict);

                /// <summary>
                /// Cria uma expressão para remover deletados
                /// "entity => EF.Property<bool>(entity, "IsDeleted") == false"
                /// </summary>
                if (entityType.ClrType.BaseType == typeof(Entity))
                {
                    ParameterExpression _parameter = Expression.Parameter(entityType.ClrType);

                    MethodInfo _propertyMethodInfo = typeof(EF).GetMethod("Property")!
                                                               .MakeGenericMethod(typeof(bool));

                    MethodCallExpression _isDeletedProperty = Expression.Call(_propertyMethodInfo,
                                                                              _parameter,
                                                                              Expression.Constant(nameof(Entity.IsDeleted)));

                    BinaryExpression _compareExpression = Expression.MakeBinary(ExpressionType.Equal,
                                                                                _isDeletedProperty,
                                                                                Expression.Constant(false));

                    LambdaExpression _lambda = Expression.Lambda(_compareExpression,
                                                                 _parameter);

                    /// <summary>
                    /// Ignora atualização da propriedade "CreatedDate"
                    /// Referências:
                    /// https://www.learnentityframeworkcore.com/configuration/fluent-api/valuegeneratedonaddorupdate-method
                    /// https://learn.microsoft.com/pt-br/dotnet/api/microsoft.entityframeworkcore.metadata.builders.propertybuilder-1.valuegeneratedonupdate
                    /// </summary>
                    builder.Entity(entityType.ClrType)
                           .Property(nameof(Entity.CreatedDate))
                           .ValueGeneratedOnUpdate();

                    builder.Entity(entityType.ClrType)
                           .HasQueryFilter(_lambda);
                }

                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    switch (property.Name)
                    {
                        case nameof(Entity.Id):
                            property.IsKey();
                            break;

                        case nameof(Entity.ModifiedDate):
                            property.IsNullable = true;
                            break;

                        //https://docs.microsoft.com/en-us/ef/core/saving/explicit-values-generated-properties#setting-an-explicit-value-during-update
                        case nameof(Entity.CreatedDate):
                            property.IsNullable = false;
                            property.SetDefaultValueSql("GETDATE()");
                            property.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
                            break;

                        case nameof(Entity.IsDeleted):
                            property.IsNullable = false;
                            property.SetDefaultValue(false);
                            break;
                    }

                    if (property.ClrType == typeof(string) && string.IsNullOrEmpty(property.GetColumnType()))
                        property.SetColumnType($"varchar({property.GetMaxLength() ?? 100})");

                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                        property.SetColumnType("datetime");
                }
            }

            return builder;
        }

        #endregion Metodos Publicos
    }
}