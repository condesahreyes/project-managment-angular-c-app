export abstract class constants {

   static readonly columnName = 'name';
   static readonly columnDomain = 'domain';
   static readonly columnVersion = 'version';
   static readonly columnState = 'state';
   static readonly columnDuration = 'duration';
   static readonly columnActions = 'actions';
   static readonly columnTotalBugs = 'TotalBugs';
   static readonly columnPrice = 'price';
   static readonly columnId = 'id';
   static readonly columnProject = 'project';



   static readonly actionCreate = 'create';
   static readonly actionEdit = 'edit';
   static readonly actionDelete = 'delete';
   static readonly actionImport = 'import';
   static readonly actionEditState = 'editState';
   static readonly actionAssign = 'assign';
   static readonly actionVisibility = 'visibility';

   static readonly rolDeveloper = 'Desarrollador';
   static readonly rolTester = 'Tester';
   static readonly rolAdmin = 'Administrador';


   static readonly bugColumns = [constants.columnName, constants.columnId, constants.columnProject, constants.columnDomain, 
      constants.columnVersion, constants.columnState, constants.columnDuration, constants.columnActions]; 

   static readonly projectColumns =  [constants.columnName, constants.columnTotalBugs, 
      constants.columnDuration, constants.columnPrice, constants.columnActions];

   static readonly projectColumnsDeveloperTester =  [constants.columnName, constants.columnActions];


   static readonly bugActions = [constants.actionCreate, constants.actionEdit, 
      constants.actionDelete, constants.actionImport]; 

   static readonly bugActionsTester = [constants.actionCreate, constants.actionEdit, constants.actionDelete];

   static readonly bugActionsDeveloper = [constants.actionEditState];

   static readonly projectActions = [constants.actionAssign, constants.actionEdit, constants.actionDelete, 
      constants.actionVisibility, constants.actionCreate];
   
   static readonly projectActionsDeveloperTester = [constants.actionVisibility]; 

};