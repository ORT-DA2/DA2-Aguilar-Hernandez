export enum AuthEndpoints {
  LOGIN = '/auth/login',
  REGISTER = '/auth/register',
  ADMIN = '/auth/admin',
  USER = '/auth/user',
}

export enum ArticleEndpoints {
  LAST_ARTICLES = '/articles/last-articles',
  ARTICLES = '/articles',
  SEARCH_ARTICLES = '/articles/search',
  ALL_USER_ARTICLES = '/articles',
  ALL_PUBLIC_ARTICLES = '/articles/public',
}

export enum UserEndpoints {
  GET_USER = '/users',
  EDIT_USER = '/users',
  GET_RANKING_OFFENSIVE = '/users/rankingOffensive',
  GET_RANKING_ACTIVITY = '/users/ranking',
}

export enum OffensiveEndpoints {
  GET_OFFENSIVE = '/OffensiveWords',
  ADD_OFFENSIVE = '/OffensiveWords',
  REMOVE_OFFENSIVE = '/OffensiveWords',
}
