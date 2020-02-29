import React, {useState, useRef, useEffect} from 'react';
import PropTypes from 'prop-types';
import withStyles from '@material-ui/core/styles/withStyles';

const styles = {
  form: {
    display: 'flex',
    flexDirection: 'row',
    alignItems: 'flex-start',
    justifyContent: 'flex-start',
    border: '1px solid rgb(224, 224, 224)',
  },
  textarea: {
    outline: 0,
    height: '50px',
    width: '100%',
    resize: 'none',
    border: 0,
    paddingLeft: 10,
    paddingTop: 5,
    fontSize: 14,
  },
  button: {
    letterSpacing: '0.5px',
    outline: 0,
    transition: ' opacity 0.1s',
    border: 0,
    color: 'rgb(158, 158, 158)',
    fontSize: 14,
    borderRadius: 5,
    padding: 10,
    backgroundColor: 'transparent',
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
    fontWeight: 400,
    whiteSpace: 'nowrap',
    alignSelf: 'flex-start',
  },
};

/**
 * Creates a comment for a post
 */
const CreateComment = ({focus, classes, addAnswer}) => {
  const [comment, setComment] = useState('');
  const buttonEl = useRef(null);
  const TextareaEl = useRef(false);

  useEffect(() => {
    focus && TextareaEl.current.focus();
  }, [focus]);

  const handleSubmit = async e => {
    e.preventDefault();
    await addAnswer(comment);
    setComment('');
  };

  const onEnterPress = e => {
    if (e.keyCode === 13) {
      e.preventDefault();
      buttonEl.current.click();
    }
  };

  return (
    <form className={classes.form} onSubmit={e => handleSubmit(e)}>
      <textarea
        className={classes.textarea}
        onChange={e => setComment(e.target.value)}
        value={comment}
        placeholder="Add a answer..."
        onKeyDown={onEnterPress}
        ref={TextareaEl}
      />

      <button
        className={classes.button}
        type="submit"
        color={comment ? 'primary.main' : 'grey[500]'}
        weight="bold"
        text
        ref={buttonEl}
        disabled={!comment}
      >
        Post
      </button>
    </form>
  );
};

CreateComment.propTypes = {
  focus: PropTypes.bool,
  classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(CreateComment);
