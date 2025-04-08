import { Group } from './group';
import { User } from './user';

export interface Note {
  id?: number;
  subjectGroupId: number;
  studentId: string;
  url: null;
  date: string;
  student: User;
  subjectGroup: Group;
}
