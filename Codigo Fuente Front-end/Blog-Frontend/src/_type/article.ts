import { User } from './user';

export interface Article {
  id: string;
  title: string;
  content: string;
  isPublic: boolean;
  ownerId: string;
  datePublished: number;
  dateLastModified: number;
  comments?: string[];
  image: string;
  template: string;
  isApproved: boolean;
  isEdited: boolean;
  offensiveContent: string[];
}
