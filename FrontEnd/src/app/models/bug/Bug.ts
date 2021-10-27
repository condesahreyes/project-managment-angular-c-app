import { Guid } from 'guid-typescript';
export interface Bug {
  "Project": string;
  "Id": number;
  "Name": string;
  "Domain": string;
  "Version": string;
  "State": string;
 // "CreatedBy": Guid;
}