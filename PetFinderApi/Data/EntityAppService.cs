﻿using Microsoft.EntityFrameworkCore;
using PetFinderApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinderApi.Data
{
    public class EntityAppService
    {
        public readonly FinderContext _DbFinder;

        public EntityAppService(FinderContext _context)
        {
            _DbFinder = _context;
        }

        public async Task<Boolean> GetByAuth(string authId)
        {
            var entity = await _DbFinder.Entities.FirstOrDefaultAsync(x => x.auth0Id == authId);

            if(entity != null)
            {
                return true;
            }

            return false;
        }

        public async Task<string> PostEntity(Entity entity)
        {
            var email = await _DbFinder.Entities.FirstOrDefaultAsync(e => e.Email == entity.Email);

            var identidad = await _DbFinder.Entities.FirstOrDefaultAsync(e => e.Identification == entity.Identification);

            var user = await _DbFinder.Entities.FirstOrDefaultAsync(u => u.UserName == entity.UserName);


            if (email != null)
            {
                return "Email Exists!";
            }

            if (user != null)
            {
                return "UserName Exists!";
            }

            if (identidad != null)
            {
                return "ID Duplicated!";
            }

            _DbFinder.Entities.Add(entity);

            await _DbFinder.SaveChangesAsync();

            return null;
        }

        public async Task<string> PutEntity(Entity entity, long id)
        {
            if (id != entity.idEntity)
            {
                return "Not Found";
            }

            var idEntidad = await _DbFinder.Entities.FirstOrDefaultAsync(i => i.idEntity == entity.idEntity);

            if (idEntidad == null)
            {
                return "ID doesnt exists!";
            }

            return null;
        }
    }
}
