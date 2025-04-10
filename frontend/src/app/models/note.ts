import { Group } from './group';
import { NoteFile } from './noteFile';
import { User } from './user';

export interface Note {
  id?: number;
  subjectGroupId: number;
  studentId: string;
  noteFiles: NoteFile[];
  date: string;
  student: User;
  subjectGroup: Group;
  week: number;
}
