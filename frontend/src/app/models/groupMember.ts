import { Group } from './group';
import { User } from './user';

export interface GroupMember {
  id: number;
  studentId: string;
  subjectGroupId: 1;
  student: User;
  subjectGroup: Group;
}
