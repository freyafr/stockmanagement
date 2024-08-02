<template>
  <div class="account-info-container">
    <h2>User Account Information</h2>
    <div class="account-details">
      <p><strong>Name:</strong> {{ user.name }} {{user.lastName}}</p>
      <p><strong>Email:</strong> {{ user.email }}</p>
      <p><strong>Balance:</strong> ${{ Number(user.balance).toFixed(2) }}</p>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      user: {},
    };
  },
  async created() {
    await this.fetchUserAccount();
  },
  methods: {
    async fetchUserAccount() {
      try {
        const response = await fetch('https://localhost:7252/api/v1/Accounts/89e8d857-3e13-4d47-b53b-04b0eda248ea');
        this.user = await response.json();
      } catch (error) {
        console.error('Error fetching user account:', error);
      }
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
/* Container for the account information */
.account-info-container {
  margin-top: 20px;
  padding: 20px;
  max-width: 500px;
  background-color: #f1f1f1;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

/* Styling for the account details */
.account-details {
  margin-top: 10px;
}

.account-details p {
  margin: 10px 0;
  font-size: 16px;
  color: #333;
}

/* Strong text styling */
.account-details strong {
  color: #007bff;
}

/* Header styling */
.account-info-container h2 {
  margin-bottom: 20px;
  font-size: 24px;
  color: #333;
  border-bottom: 2px solid #007bff;
  padding-bottom: 10px;
}

/* Media query for responsiveness */
@media (max-width: 600px) {
  .account-info-container {
    padding: 15px;
    max-width: 100%;
  }

  .account-details p {
    font-size: 14px;
  }

  .account-info-container h2 {
    font-size: 20px;
  }
}
</style>
