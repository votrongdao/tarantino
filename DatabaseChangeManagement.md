# **Database change management** #
## Introduction ##

Tarantino Database change management provides a set of tools which make the process of propagating database schema and data changes to multiple environments frictionless.

For a detailed presentation on this subject look at this: [Database Change Management](http://tarantino.googlecode.com/svn/docs/Database-Change-Management.ppt)


### The problem that Database Change Management is attempting to solve: ###
  * Most significant business applications rely on at least one relational database for persisting data
  * As new features are developed, database schema changes are often necessary – i.e. new tables, columns, views, and stored procedures
  * Database schema changes and corresponding code changes must always be deployed together
  * While deploying software to a production environment, code files and libraries may usually be deleted or overwritten – Database files, however, must be intelligently manipulated so as not destroy vital business data

> The development tools available allow developers to make changes to their environement and do not address the problem of applying those changes to additional environments. (i.e. development, quality assurance, staging, production).

### The solution to this problem: ###
Successful database change management requires that a consistent process be applied by all team members.  Without a consistent process than the tools provided in this solution will not provide its full value.

The proposed/ideal process that uses the Tarantino Database change management tools, would consist of:
  * Each developer using their own local database to do their development work.
  * Each environment using it's own database.  i.e. Development, Testing, Staging, Production
  * Each developer maintains his changes locally. When the database changes are ready to commit along side the application source code, the developer follows these steps:
    * Create a change script that wraps all of the database changes into a single transactional change script.  A Tool like Red Gate SQL Compare makes this a 30 second operation.
    * Save the change script in to a folder in your source tree call Update.
    * Commit the change script along with the source code that corresponds to the change.
  * The continous integration server detects changes to the source control repository than it:
    * It builds the application code.
    * It executes the applications unit tests.
    * Executes the database create task to create a new database with all of the changes that are in source control.
    * Executes the projects integration (data access) tests.
    * Marks the buils a success when all the tests pass.
  * Each developer runs the build script locally after receiving new schema changes scripts from the source code repository.
  * The testers, developers, change management managers execute the script using the Database Update (windows form) tool to run the database scripts against the Dev, Test, and staging environments when the environments receive their next update of the source code.

### Prerequisites ###
The following environmental conditions make the use of the database change managment process frictionless.
  * An automated build script.  This includes compillation, unit tests, integration tests, versioning, packaging, publishing
  * Source Control (why do some people still think a zip file is source control?)
  * A continous integration server that has a seperate database instance dedicated to executing schema change scripts and integration tests
  * A team that believes that a little process can go a long way.
  * Seperate environments for testers, marketing/content types, and staging.
  * The will to do things better

## Details ##
To use the manage database tasks the following assemblies are need.  Place these in your NAnt binary directory:

  * Tarantino.Core.dll
  * Tarantino.DatabaseManager.Tasks.dll
  * Microsoft.SqlServer.BatchParser.dll
  * Microsoft.SqlServer.ConnectionInfo.dll
  * Microsoft.SqlServer.Replication.dll
  * Microsoft.SqlServer.Smo.dll
  * StructureMap.dll

---


  * The system is based on a set of conventions which allows incremental changes to a database schema.

The conventions are to to create two subdirectories in you Database Scripts folder
Create - This is where your initial Schema change scripts.
Update - This is where your change scripts should be placed.  They should be named with the following convention ####-SCRIPTNAME.sql  where #### is the script number with leading zeros.  This will ensure that the first script 0001-firstSchemaChange.sql would get exectured first.

```
		<manageSqlDatabase
			scriptDirectory="${database.script.directory}"
			action="${action}"
			server="${database.server}"
			integratedAuthentication="${database.integrated}"
			database="${database.name}"
			username="${database.username}"
			password="${database.password}"
		/>
```