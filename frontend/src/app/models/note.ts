import { Group } from './group';
import { NoteFile } from './noteFile';
import { User } from './user';

export interface Note {
  id?: number;
  title: string;
  subjectGroupId: number;
  studentId: string;
  noteFiles: NoteFile[];
  date: string;
  student: User;
  subjectGroup: Group;
  week: number;
}
