import { User } from '../users/User';
import { Bug } from '../bug/Bug';

export interface ProjectOut {
  "Id": number;
  "Users" : User[];
  "Bugs" : Bug[];
  "TotalBugs" : number;
  "Name" : string;
}