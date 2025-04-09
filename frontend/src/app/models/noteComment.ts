import { User } from './user';

export interface NoteComment {
  id: number;
  noteId: number;
  authorId: string;
  content: string;
  author: User;
  date: string;
}
