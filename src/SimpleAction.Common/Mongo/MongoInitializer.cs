using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace SimpleAction.Common.Mongo {
    public class MongoInitializer : IDatabaseInitializer {
        private bool _initialized = false;
        private readonly IMongoDatabase _database;
        private readonly IDatabaseSeeder _seeder;
        private readonly bool _seed;

        public MongoInitializer (IMongoDatabase database,
            IOptions<MongoOptions> options,
            IDatabaseSeeder seeder) {
            _database = database;
            this._seeder = seeder;
            _seed = options.Value.Seed;
        }

        public async Task InitializeAsync () {

            if (_initialized) {
                return;
            }
            RegisterConventions ();
            if (!_seed) {
                return;
            }
            await _seeder.SeedAsync ();

            // await Task.Run(() => {
            //     if (_initialized) {
            //         return;
            //     }
            //     RegisterConventions ();
            //     if (!_seed) {
            //         return;
            //     }

            // });

        }

        private void RegisterConventions () {
            ConventionRegistry.Register ("SimpleActionConventions", new MongoConvention (), x => true);
        }

        private class MongoConvention : IConventionPack {
            public IEnumerable<IConvention> Conventions => new List<IConvention> () {
                new IgnoreExtraElementsConvention (true),
                new EnumRepresentationConvention (BsonType.String),
                new CamelCaseElementNameConvention ()
            };
        }
    }
}