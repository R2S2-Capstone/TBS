const shipperUtilities = {
  parsePostStatus: (status) => {
    if (status === 0) {
      return 'Open'
    } else if (status === 1) {
      return 'Closed'
    } else if (status === 2) {
      return 'Pending Delivery'
    } else if (status === 3) {
      return 'Pending Delivery Approval'
    } else {
      return 'Unknonw'
    }
  },
}
export default shipperUtilities