import React, {Component} from 'react';
import MyButton from '../../util/MyButton';
import {Link} from 'react-router-dom';
import PropTypes from 'prop-types';
// Icons
import FavoriteIcon from '@material-ui/icons/Favorite';
import FavoriteBorder from '@material-ui/icons/FavoriteBorder';
// REdux
import {connect} from 'react-redux';

import {likeScream, unlikeScream} from '../../redux/actions/QuestionDetailActions';

export class LikeButtonQuestionDetail extends Component {
  likeScream = () => {
    this.props.likeScream(this.props.answerId);
  };
  unlikeScream = () => {
    this.props.unlikeScream(this.props.answerId);
  };
  render() {
    const {isVote} = this.props;

    console.log('isVote', isVote);

    const likeButton = isVote ? (
      <MyButton tip="Undo like" onClick={this.unlikeScream}>
        <FavoriteIcon color="primary" />
      </MyButton>
    ) : (
      <MyButton tip="Like" onClick={this.likeScream}>
        <FavoriteBorder color="primary" />
      </MyButton>
    );
    return likeButton;
  }
}

LikeButtonQuestionDetail.propTypes = {
  user: PropTypes.object.isRequired,
  answerId: PropTypes.number.isRequired,
  isVote: PropTypes.bool.isRequired,
  likeScream: PropTypes.func.isRequired,
  unlikeScream: PropTypes.func.isRequired,
};

const mapStateToProps = state => ({
  user: state.user,
});

const mapActionsToProps = {
  likeScream,
  unlikeScream,
};

export default connect(mapStateToProps, mapActionsToProps)(LikeButtonQuestionDetail);
