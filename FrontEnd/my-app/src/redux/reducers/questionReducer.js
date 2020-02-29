import {
    ADD_COMMENT_FOR_ANSWER,
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
      case ADD_COMMENT_FOR_ANSWER:
        let index = findIndex(state.questions, [
            'answerId',
            action.payload.answerId,
          ]);
          state.questions[index].isVotedForAnswer = false;
          state.questions[index].totalVote = action.payload.totalVote;
        return {
          ...state,
          questions: concat(state.questions, action.payload.questions),
          totalQuestion: action.payload.totalQuestion,
          loading: false,
        };
      
      default:
        return state;
    }
  }
  