import {
  LIKE_SCREAM,
  UNLIKE_SCREAM,
  LOADING_DATA,
  DELETE_SCREAM,
  POST_SCREAM,
  SUBMIT_COMMENT,
  SET_QUESTIONS,
  SET_CATEGORIES,
  RESET_HOME,
} from '../types';

import {concat, uniqBy, findIndex} from 'lodash';

const initialState = {
  scream: {},
  questions: [],
  totalQuestion: 0,
  loading: false,
  categories: [],
};

export default function(state = initialState, action) {
  switch (action.type) {
    case LOADING_DATA:
      return {
        ...state,
        loading: true,
      };
    case SET_QUESTIONS:
      return {
        ...state,
        questions: uniqBy(
          concat(state.questions, action.payload.questions),
          'questionId'
        ),
        totalQuestion: action.payload.totalQuestion,
        loading: false,
      };
    case SET_CATEGORIES:
      return {
        ...state,
        categories: action.payload,
        loading: false,
      };
    case LIKE_SCREAM:
      const index = findIndex(state.questions, [
        'answerId',
        action.payload.answerId,
      ]);

      state.questions[index].isVotedForAnswer = true;
      state.questions[index].totalVote = action.payload.totalVote;
      return {
        ...state,
      };
    case UNLIKE_SCREAM:
      const idx = findIndex(state.questions, [
        'answerId',
        action.payload.answerId,
      ]);
      state.questions[idx].isVotedForAnswer = false;
      state.questions[idx].totalVote = action.payload.totalVote;
      return {
        ...state,
      };
    case DELETE_SCREAM:
      index = state.screams.findIndex(
        scream => scream.screamId === action.payload
      );
      state.screams.splice(index, 1);
      return {
        ...state,
      };
    case POST_SCREAM:
      return {
        ...state,
        screams: [action.payload, ...state.screams],
      };
    case SUBMIT_COMMENT:
      return {
        ...state,
        scream: {
          ...state.scream,
          comments: [action.payload, ...state.scream.comments],
        },
      };
    case SET_QUESTIONS:
      return {
        ...state,
        questions: action.payload,
        loading: false,
      };
    case RESET_HOME:
      return initialState;
    default:
      return state;
  }
}
