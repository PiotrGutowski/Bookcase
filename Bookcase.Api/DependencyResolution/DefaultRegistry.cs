// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Bookcase.Core.Repositories;
using Bookcase.Infrastructure.Repositories;

namespace Bookcase.Api.DependencyResolution {
    using AutoMapper;
    using Bookcase.Infrastructure.BookcaseDbContext;
    using Bookcase.Infrastructure.Mappers;
    using Bookcase.Infrastructure.Services;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
	
    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            For<IBookRepository>().Use<BookRepository>();
            For<IBookService>().Use<BookService>();
            For<IAuthorRepository>().Use<AuthorRepository>();
            For<IAuthorService>().Use<AuthorService>();
            For<IBorrowedBooksRepository>().Use<BorrowedBooksRepository>();
            For<IBorrowedBooksService>().Use<BorrowedBooksService>();
            For<IUserRepository>().Use<UserRepository>();
            For<IUserService>().Use<UserService>();
            For<BookcaseContext>().Use(() => new BookcaseContext());
            For<IMapper>().Use(() => AutoMapperConfig.Initialize());
        }

        #endregion
        
          
        
    }
}