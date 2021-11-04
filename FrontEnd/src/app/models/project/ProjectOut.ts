import { User } from '../users/User';
import { Bug } from '../bug/Bug';

export interface ProjectOut {
  "id": number;
  "users" : User[];
  "bugs" : Bug[];
  "totalBugs" : number;
  "name" : string;
  "price" : number;
  "duration" : number;
}