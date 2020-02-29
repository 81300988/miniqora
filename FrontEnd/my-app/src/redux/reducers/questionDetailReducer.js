import {
  LOADING_DATA,
  SET_QUESTION,
  SET_CATEGORY_QUESTIONS,
  RESET_CATEGORY_PAGE,
  LIKE_SCREAM_DETAIL,
  UNLIKE_SCREAM_DETAIL,
} from '../types';

import {concat, findIndex} from 'lodash';

const initialState = {
  question: {},
  questions: [],
  totalQuestion: 0,
  loading: false,
};

export default function(state = initialState, action) {
  switch (action.type) {
    case SET_CATEGORY_QUESTIONS:
      return {
        ...state,
        questions: concat(state.questions, action.payload.questions),
        totalQuestion: action.payload.totalQuestion,
        loading: false,
      };
    case SET_QUESTION:
      console.log('SET_QUESTION', action.payload);
      return {
        ...state,
        question: action.payload,
        loading: false,
      };
    case LOADING_DATA:
      return {
        ...state,
        loading: true,
      };
    case LIKE_SCREAM_DETAIL:
      const index = findIndex(state.question.answers, [
        'answerId',
        action.payload.answerId,
      ]);
      state.question.answers[index].isVotedForAnswer = true;
      state.question.answers[index].totalVote = action.payload.totalVote;
      return {
        ...state,
      };
    case UNLIKE_SCREAM_DETAIL:
      const idx = findIndex(state.question.answers, [
        'answerId',
        action.payload.answerId,
      ]);
      state.question.answers[idx].isVotedForAnswer = false;
      state.question.answers[idx].totalVote = action.payload.totalVote;
      return {
        ...state,
      };
    case RESET_CATEGORY_PAGE:
      return initialState;
    default:
      return state;
  }
}
