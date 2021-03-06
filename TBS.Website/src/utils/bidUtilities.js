const bidUtilities = {
  parseBidStatus: (status) => {
    if (status === 0) {
      return 'Cancelled'
    } else if (status === 1) {
      return 'Declined'
    } else if (status === 2) {
      return 'Completed'
    } else if (status === 3) {
      return 'Open' 
    } else if (status === 4) {
      return 'Pending Delivery'
    } else if (status === 5) {
      return 'Pending Delivery Approval'
    } else {
      return 'Unknown'
    }
  },
  reverseBidStatus: (status) => {
    if (status == 'cancelled') {
      return 0
    } else if (status == 'declined') {
      return 1
    } else if (status == 'completed') {
      return 2
    } else if (status == 'open') {
      return 3
    } else if (status == 'pending delivery') {
      return 4
    } else if (status == 'pending delivery approval') {
      return 5
    } else {
      return 'Unknown'
    }
  },
}
export default bidUtilities