import { GroupMember } from './groupMember';
import { Note } from './note';

export interface Group {
  id: number;
  code: string;
  name: string;
  credits: number;
  scheduleText: string;
  teacherName: string;
  teacherId: number | null;
  teacher: any | null;
  notes: Note[];
  members: GroupMember[];
}
