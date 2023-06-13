export interface Comment{
  id: string,
  articleId: string,
  ownerUsername: string | null,
  body: string,
  reply?: string,
  isPublic: boolean,
  isApproved: boolean,
  isEdited: boolean,
  datePublished: number,
  offensiveWords: string[]
}
