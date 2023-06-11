export interface Article {
  id: string;
  title: string;
  content: string;
  isPublic: boolean;
  owner: string;
  datePublished: number;
  dateLastModified: number;
  comments?: string[];
  template: string;
  isApproved: boolean;
  isEdited: boolean;
  offensiveContent: string[];
  image: string;
  image2: string;
}
