import { User } from './user';

export interface Article {
  id: string;
  title: string;
  content: string;
  isPublic: boolean;
  owner: string;
  datePublished: Date;
  dateLastModified: Date;
  comments?: string[];
  image: string;
  template: string;
  isApproved: boolean;
  isEdited: boolean;
  offensiveContent: string[];
}
