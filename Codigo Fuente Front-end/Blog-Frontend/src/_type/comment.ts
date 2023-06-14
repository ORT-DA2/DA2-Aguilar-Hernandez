import { OffensiveWord } from './offensiveWord';

export interface Comment {
  id: string;
  articleId: string;
  ownerUsername: string | undefined;
  body: string;
  reply?: string;
  isPublic: boolean;
  isApproved: boolean;
  isEdited: boolean;
  datePublished: number;
  offensiveContent: OffensiveWord[];
  isOffensive: boolean;
}
