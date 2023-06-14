import { Comment } from './comment';
import { OffensiveWord } from './offensiveWord';

export interface Article {
  id: string;
  title: string;
  content: string;
  isPublic: boolean;
  owner: string;
  datePublished: number;
  dateLastModified: number;
  comments?: Comment[];
  template: string;
  isApproved: boolean;
  isEdited: boolean;
  offensiveContent: OffensiveWord[];
  image: string;
  image2: string;
}
