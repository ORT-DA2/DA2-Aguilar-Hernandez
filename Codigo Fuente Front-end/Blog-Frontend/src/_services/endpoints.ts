export enum AuthEndpoints {
  LOGIN = '/auth/login',
  REGISTER = '/auth/register',
  ADMIN = '/auth/admin',
  USER = '/auth/user',
}

export enum ArticleEndpoints {
  LAST_ARTICLES = '/articles/last-articles',
  SEARCH_ARTICLES = '/articles/search',
}

export enum UserEndpoints {
  GET_USER = '/users',
  EDIT_USER = '/users',
}

export enum OffensiveEndpoints {
  GET_OFFENSIVE = '/OffensiveWords',
  ADD_OFFENSIVE = '/OffensiveWords',
  REMOVE_OFFENSIVE = '/OffensiveWords',
}
